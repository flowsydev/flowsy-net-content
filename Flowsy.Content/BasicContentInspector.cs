using System.Collections.Immutable;
using Flowsy.Core;

namespace Flowsy.Content;

public class BasicContentInspector : IContentInspector
{
    public virtual ContentDescriptor Inspect(object content)
        => content switch
        {
            Stream stream => Inspect(stream),
            ImmutableArray<byte> immutableArray => Inspect(immutableArray),
            byte[] array => Inspect(ImmutableArray.Create(array)),
            _ => throw new NotSupportedException()
        };

    public virtual ContentDescriptor Inspect(string filePath)
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        
        var contentDescriptor = Inspect(stream);
        
        var fileInfo = new FileInfo(filePath);
        contentDescriptor.Name = fileInfo.Name;
        contentDescriptor.Location = fileInfo.DirectoryName;
        contentDescriptor.CreationDate = fileInfo.CreationTime; 
        contentDescriptor.ModificationDate = fileInfo.LastWriteTime; 
        contentDescriptor.ReadDate = fileInfo.LastAccessTime;
        contentDescriptor.ByteLength = fileInfo.Length;
        contentDescriptor.MetaData = new Dictionary<string, object?>
        {
            ["Attributes"] = fileInfo.Attributes,
            ["Exists"] = fileInfo.Exists,
            ["IsReadOnly"] = fileInfo.IsReadOnly,
            ["LinkTarget"] = fileInfo.LinkTarget,
            ["Extension"] = fileInfo.Extension
        };

        return contentDescriptor;
    }

    public virtual ContentDescriptor Inspect(Stream stream)
    {
        if (stream.CanSeek)
            return Inspect(ImmutableArray.Create(stream.ToArray()));

        return new ContentDescriptor
        {
            ByteLength = stream.Length
        };
    }

    public virtual ContentDescriptor Inspect(ImmutableArray<byte> array)
        => new()
        {
            ByteLength = array.Length
        };
}