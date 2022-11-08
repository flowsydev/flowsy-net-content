using System.Collections.Immutable;
using Flowsy.Core;
using HeyRed.Mime;

namespace Flowsy.Content;

public class BasicContentInspector : IContentInspector
{ 
    public virtual ContentDescriptor Inspect(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        var contentDescriptor = Inspect(stream, fileInfo.Extension);
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

    public virtual ContentDescriptor Inspect(Stream stream, string? fileExtension = null)
        => Inspect(stream.ToArray(), fileExtension);

    public virtual ContentDescriptor Inspect(ImmutableArray<byte> bytes, string? fileExtension = null)
        => Inspect(bytes.AsEnumerable(), fileExtension);
    
    public virtual ContentDescriptor Inspect(IEnumerable<byte> bytes, string? fileExtension = null)
        => new()
        {
            ByteLength = bytes.Count(),
            MimeTypes = fileExtension is null ? Array.Empty<string>() : new [] { MimeTypesMap.GetMimeType(fileExtension) },
            Extensions =fileExtension is null ? Array.Empty<string>() : new [] { fileExtension }
        };
    
    public virtual ContentDescriptor Inspect(object content, string? fileExtension = null)
        => content switch
        {
            Stream stream => Inspect(stream, fileExtension),
            ImmutableArray<byte> bytes => Inspect(bytes, fileExtension),
            IEnumerable<byte> bytes => Inspect(bytes, fileExtension),
            _ => throw new NotSupportedException()
        };
}