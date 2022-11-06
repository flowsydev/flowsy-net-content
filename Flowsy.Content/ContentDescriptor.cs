namespace Flowsy.Content;

/// <summary>
/// Describes a given content.
/// </summary>
public class ContentDescriptor
{
    public ContentDescriptor() : this(string.Empty)
    {
    }

    public ContentDescriptor(
        string name,
        string? location = null,
        string? description = null,
        IDictionary<string, object?>? metaData = null,
        DateTime? creationDate = null,
        DateTime? modificationDate = null,
        DateTime? readDate = null,
        long? byteLength = null,
        IEnumerable<string>? mimeTypes = null,
        IEnumerable<string>? extensions = null
        )
    {
        Name = name;
        Location = location;
        Description = description;
        MetaData = metaData ?? new Dictionary<string, object?>();
        CreationDate = creationDate;
        ModificationDate = modificationDate;
        ReadDate = readDate;
        ByteLength = byteLength;
        MimeTypes = mimeTypes ?? Array.Empty<string>();
        Extensions = extensions ?? Array.Empty<string>();
    }

    /// <summary>
    /// The name for the content. For example, a file name or document identifier within a distributed system.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The location of the content. For example, a directory path, network address or web URL.
    /// </summary>
    public string? Location { get; set; }
    
    /// <summary>
    /// A description for the content.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Optional metadata for the content.
    /// </summary>
    public IDictionary<string, object?> MetaData { get; set; }

    /// <summary>
    /// The moment when the content was created.
    /// </summary>
    public DateTime? CreationDate { get; set; }
    
    /// <summary>
    /// The moment when the content was written for the last time.
    /// </summary>
    public DateTime? ModificationDate { get; set; }
    
    /// <summary>
    /// The moment when the content was read for the last time.
    /// </summary>
    public DateTime? ReadDate { get; set; }
    
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
    public IEnumerable<string> MimeTypes { get; set; }
    
    /// <summary>
    /// The first file extension associated to the content.
    /// </summary>
    public string? Extension => Extensions.FirstOrDefault();
    
    /// <summary>
    /// The file extensions associated to the content.
    /// </summary>
    public IEnumerable<string> Extensions { get; set; }
}