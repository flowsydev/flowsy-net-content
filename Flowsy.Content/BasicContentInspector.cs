using Flowsy.Core;
using HeyRed.Mime;

namespace Flowsy.Content;

public class BasicContentInspector : IContentInspector
{
    protected const char ExtensionSeparator = '.'; 
    
    protected virtual string ResolveExtension(string fileName)
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

    protected virtual void CopyInfo(FileInfo source, ContentDescriptor target)
    {
        target.Name = source.Name;
        target.Location = source.DirectoryName;
        target.CreationDate = source.CreationTime; 
        target.ModificationDate = source.LastWriteTime; 
        target.ReadDate = source.LastAccessTime;
        target.ByteLength = source.Length;
        target.MetaData = new Dictionary<string, object?>
        {
            ["Attributes"] = source.Attributes,
            ["Exists"] = source.Exists,
            ["IsReadOnly"] = source.IsReadOnly,
            ["Extension"] = source.Extension
        };
    }

    public virtual ContentDescriptor Inspect(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        var contentDescriptor = Inspect(stream, ResolveExtension(fileInfo.Name));
        CopyInfo(fileInfo, contentDescriptor);
        
        return contentDescriptor;
    }

    public virtual async Task<ContentDescriptor> InspectAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fileInfo = new FileInfo(filePath);
        var extension = ResolveExtension(filePath);
        await using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        var contentDescriptor = await InspectAsync(stream, extension, cancellationToken);
        CopyInfo(fileInfo, contentDescriptor);

        return contentDescriptor;
    }

    public virtual ContentDescriptor Inspect(Stream stream, string? fileExtension = null)
        => Inspect(stream.ToArray(), fileExtension);

    public virtual async Task<ContentDescriptor> InspectAsync(
        Stream stream,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
        )
    {
        var bytes = await stream.ToArrayAsync(cancellationToken);
        return Inspect(bytes, fileExtension);
    }
    
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

    public virtual ContentDescriptor Inspect(object content, string? fileExtension = null)
        => content switch
        {
            Stream stream => Inspect(stream, fileExtension),
            IEnumerable<byte> bytes => Inspect(bytes, fileExtension),
            _ => throw new NotSupportedException()
        };

    public virtual async Task<ContentDescriptor> InspectAsync(
        object content,
        string? fileExtension = null,
        CancellationToken cancellationToken = default
    )
    {
        return content switch
        {
            Stream stream => await InspectAsync(stream, fileExtension, cancellationToken),
            _ => Inspect(content, fileExtension)
        };
    }
}