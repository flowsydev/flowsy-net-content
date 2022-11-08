# Flowsy Content

Foundation components for content management.

This package provides only the interfaces needed by applications to inspect document content in a uniform way, but requiring them to provide their own implementation details.

The final implementation can be powered by the toolkit best suited for every use case, such as [Mime-Detective](https://www.nuget.org/packages/Mime-Detective) or [MimeKit](https://www.nuget.org/packages/MimeKit).

## Usage
```csharp
// ContentInspector.cs
// using ...
using Flowsy.Content;
// using ...

public class ContentInspector : IContentInspector
{
    public ContentDescriptor Inspect(string filePath)
    {
        ContentDescriptor descriptor;
        // Inspect the file and assign value to descriptor;
        return descriptor;
    }

    public ContentDescriptor Inspect(Stream stream, string? fileExtension = null)
    {
        ContentDescriptor descriptor;
        // Inspect the stream and assign value to descriptor;
        return descriptor;
    }
    
    public ContentDescriptor Inspect(ImmutableArray<byte> bytes, string? fileExtension = null)
    {
        ContentDescriptor descriptor;
        // Inspect the bytes and assign value to descriptor;
        return descriptor;
    }
    
    public ContentDescriptor Inspect(IEnumerable<byte> bytes, string? fileExtension = null)
    {
        ContentDescriptor descriptor;
        // Inspect the bytes and assign value to descriptor;
        return descriptor;
    }
    
    public ContentDescriptor Inspect(object content, string? fileExtension = null)
    {
        ContentDescriptor descriptor;
        // Inspect then content and assign value to descriptor;
        return descriptor;
    }
}
```
```csharp
// using ...
using Flowsy.Content;
// using ...

public class ContentValidator
{
    private readonly IContentInspector _contentInspector;
    private readonly string _allowedTypes;
    
    public SomeService(IContentInspector _contentInspector, IEnumerable<string> allowedTypes)
    {
        _contentInspector = _contentInspector;
        _allowedTypes = allowedTypes;
    }
    
    public void ValidateContent(Stream stream)
    {
        var contentDescriptor = _contentInspector.Inspect(stream);
        var intersection = contentDescriptor.MimeTypes.Intersect(_allowedTypes);
        
        if (intersection.Count() != contentDescriptor.MimeTypes.Count())
        {
            throw new ValidationException("Invalid content type.");
        }
    }
}
```
