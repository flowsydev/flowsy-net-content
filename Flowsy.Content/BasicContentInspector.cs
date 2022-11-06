using System.Collections.Immutable;
using Flowsy.Core;

namespace Flowsy.Content;

public class BasicContentInspector : IContentInspector
{
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

        return contentDescriptor;
    }

    public virtual ContentDescriptor Inspect(Stream stream)
        => Inspect(ImmutableArray.Create(stream.ToArray()));

    public virtual ContentDescriptor Inspect(ImmutableArray<byte> array)
        => new(name: string.Empty, byteLength: array.Length);
}