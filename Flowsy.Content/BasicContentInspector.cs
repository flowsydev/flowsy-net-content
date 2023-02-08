using System.Collections.Immutable;
using Flowsy.Core;
using HeyRed.Mime;

namespace Flowsy.Content;

public class BasicContentInspector : IContentInspector
{
    private const char ExtensionSeparator = '.'; 
    
    private static string ResolveExtension(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;
        
        var extension = Path.GetExtension(fileName);
        if (extension == ExtensionSeparator.ToString())
            return string.Empty;
        
        var extensionSeparatorIndex = extension.LastIndexOf(ExtensionSeparator);
        
        return extensionSeparatorIndex >= 0 && extensionSeparatorIndex + 1 < extension.Length
            ? extension[(extensionSeparatorIndex + 1)..]
            : extension;
    }

    public virtual ContentDescriptor Inspect(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        var contentDescriptor = Inspect(stream, ResolveExtension(fileInfo.Name));
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

    public virtual Task<ContentDescriptor> InspectAsync(string filePath, CancellationToken cancellationToken = default)
        => Task.Run(() => Inspect(filePath), cancellationToken);

    public virtual ContentDescriptor Inspect(Stream stream, string? fileExtension = null)
        => Inspect(stream.ToArray(), fileExtension);

    public virtual Task<ContentDescriptor> InspectAsync(
        Stream stream,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
        )
        => Task.Run(() => Inspect(stream, fileExtension), cancellationToken);

    public virtual ContentDescriptor Inspect(ImmutableArray<byte> bytes, string? fileExtension = null)
        => Inspect(bytes.AsEnumerable(), fileExtension);

    public virtual Task<ContentDescriptor> InspectAsync(
        IEnumerable<byte> bytes,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
        )
        => Task.Run(() => Inspect(bytes, fileExtension), cancellationToken);

    public virtual ContentDescriptor Inspect(IEnumerable<byte> bytes, string? fileExtension = null)
    {
        var extension = ResolveExtension(fileExtension ?? string.Empty);
        return new ContentDescriptor
        {
            ByteLength = bytes.Count(),
            MimeTypes = string.IsNullOrEmpty(extension) ? Array.Empty<string>() : new[] { MimeTypesMap.GetMimeType(extension) },
            Extensions = string.IsNullOrEmpty(extension) ? Array.Empty<string>() : new[] { extension }
        };
    }

    public virtual Task<ContentDescriptor> InspectAsync(
        ImmutableArray<byte> bytes,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
        )
        => Task.Run(() => Inspect(bytes, fileExtension), cancellationToken);

    public virtual ContentDescriptor Inspect(object content, string? fileExtension = null)
        => content switch
        {
            Stream stream => Inspect(stream, fileExtension),
            ImmutableArray<byte> bytes => Inspect(bytes, fileExtension),
            IEnumerable<byte> bytes => Inspect(bytes, fileExtension),
            _ => throw new NotSupportedException()
        };

    public virtual Task<ContentDescriptor> InspectAsync(
        object content,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
        )
        => Task.Run(() => Inspect(content, fileExtension), cancellationToken);
}