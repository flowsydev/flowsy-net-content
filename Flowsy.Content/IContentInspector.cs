using System.Collections.Immutable;

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
    /// Inspects a stream of content to obtain the corresponding descriptor.
    /// </summary>
    /// <param name="stream">The content stream.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(Stream stream);
    
    /// <summary>
    /// Inspects a byte array to obtain the corresponding content descriptor.
    /// </summary>
    /// <param name="array">The file content as a byte array.</param>
    /// <returns>An instance of ContentDescriptor.</returns>
    public ContentDescriptor Inspect(ImmutableArray<byte> array);
}