namespace Flowsy.Content;

/// <summary>
/// Describes a given content.
/// </summary>
public class ContentDescriptor
{
    public ContentDescriptor(
        string name,
        string? location = null,
        string? description = null,
        IEnumerable<string>? tags = null,
        DateTime? created = null,
        DateTime? lastWritten = null,
        DateTime? lastRead = null,
        long? byteLength = null,
        IEnumerable<string>? mimeTypes = null,
        IEnumerable<string>? extensions = null
        )
    {
        Name = name;
        Location = location;
        Description = description;
        Tags = tags ?? Array.Empty<string>();
        Created = created;
        LastWritten = lastWritten;
        LastRead = lastRead;
        ByteLength = byteLength;
        MimeTypes = mimeTypes ?? Array.Empty<string>();
        Extensions = extensions ?? Array.Empty<string>();
    }

    /// <summary>
    /// The name for the content. For example, a file name or document identifier within a distributed system.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// The location of the content. For example, a directory path, network address or web URL.
    /// </summary>
    public string? Location { get; }
    
    /// <summary>
    /// A description for the content.
    /// </summary>
    public string? Description { get; }
    
    /// <summary>
    /// Optional tags for the content.
    /// </summary>
    public IEnumerable<string> Tags { get; }
    
    /// <summary>
    /// The moment when the content was created.
    /// </summary>
    public DateTime? Created { get; }
    
    /// <summary>
    /// The moment when the content was written for the last time.
    /// </summary>
    public DateTime? LastWritten { get; }
    
    /// <summary>
    /// The moment when the content was read for the last time.
    /// </summary>
    public DateTime? LastRead { get; }
    
    /// <summary>
    /// The content size in bytes.
    /// </summary>
    public long? ByteLength { get; set; }
    
    /// <summary>
    /// The content size in kibibytes.
    /// </summary>
    public double? KibibyteLength => ByteLength / 1024d;
    
    /// <summary>
    /// The content size in kilobytes.
    /// </summary>
    public double? KilobyteLength => ByteLength / 1000d;

    /// <summary>
    /// The content size in mebibytes.
    /// </summary>
    public double? MebibyteLength => KibibyteLength / 1024d;
    
    /// <summary>
    /// The content size in megabytes.
    /// </summary>
    public double? MegabyteLength => KilobyteLength / 1000d;
    
    /// <summary>
    /// The content size in gibibytes.
    /// </summary>
    public double? GibibyteLength => MebibyteLength / 1024d;
    
    /// <summary>
    /// The content size in gigabytes.
    /// </summary>
    public double? GigabyteLength => MegabyteLength / 1000d;

    /// <summary>
    /// The first MIME type associated to the content.
    /// </summary>
    public string? MimeType => MimeTypes.FirstOrDefault();
    
    /// <summary>
    /// The MIME types associated to the content.
    /// </summary>
    public IEnumerable<string> MimeTypes { get; }
    
    /// <summary>
    /// The first file extension associated to the content.
    /// </summary>
    public string? Extension => Extensions.FirstOrDefault();
    
    /// <summary>
    /// The file extensions associated to the content.
    /// </summary>
    public IEnumerable<string> Extensions { get; }
}