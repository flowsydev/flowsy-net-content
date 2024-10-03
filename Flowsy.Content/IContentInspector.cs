namespace Flowsy.Content;

/// <summary>
/// Represents a service able to inspect a given content to obtain the corresponding descriptor.
/// </summary>
public interface IContentInspector
{
    /// <summary>
    /// Inspects the content of a file to obtain the corresponding descriptor.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(string filePath);

    /// <summary>
    /// Asynchronously inspects the content of a file to obtain the corresponding descriptor.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public Task<ContentDescriptor> InspectAsync(string filePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inspects a stream of content to obtain the corresponding descriptor.
    /// An optional file extension can be used as a fallback if the descriptor could not be determined from the stream content. 
    /// </summary>
    /// <param name="stream">The content stream.</param>
    /// <param name="fileExtension">The file extension associated with the stream.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(Stream stream, string? fileExtension = null);

    /// <summary>
    /// Asynchronously inspects a stream of content to obtain the corresponding descriptor.
    /// An optional file extension can be used as a fallback if the descriptor could not be determined from the stream content. 
    /// </summary>
    /// <param name="stream">The content stream.</param>
    /// <param name="fileExtension">The file extension associated with the stream.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public Task<ContentDescriptor> InspectAsync(Stream stream, string? fileExtension = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Inspects a byte array to obtain the corresponding content descriptor.
    /// The file extension will be used as a fallback if the descriptor could not be determined from the byte array.
    /// </summary>
    /// <param name="bytes">The file content as a byte array.</param>
    /// <param name="fileExtension">The file extension associated with the stream.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(IEnumerable<byte> bytes, string? fileExtension = null);
    
    /// <summary>
    /// Asynchronously inspects a byte array to obtain the corresponding content descriptor.
    /// The file extension will be used as a fallback if the descriptor could not be determined from the byte array.
    /// </summary>
    /// <param name="bytes">The file content as a byte array.</param>
    /// <param name="fileExtension">The file extension associated with the stream.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public Task<ContentDescriptor> InspectAsync(IEnumerable<byte> bytes, string? fileExtension = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Inspects the given content to obtain the corresponding descriptor.
    /// An optional file extension can be used as a fallback if the descriptor could not be determined from the stream content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="fileExtension">The file extension associated with the content.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(object content, string? fileExtension = null);
    
    /// <summary>
    /// Asynchronously inspects the given content to obtain the corresponding descriptor.
    /// An optional file extension can be used as a fallback if the descriptor could not be determined from the stream content.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="fileExtension">The file extension associated with the content.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public Task<ContentDescriptor> InspectAsync(object content, string? fileExtension = null, CancellationToken cancellationToken = default);
}