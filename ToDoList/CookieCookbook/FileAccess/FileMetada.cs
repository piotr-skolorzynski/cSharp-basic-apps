namespace CookieCookbook.FileAccess;

public class FileMetada
{
    public string Name { get; }
    public FileFormat Format { get; }

    public FileMetada(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    public string ToPath() =>
        Format == FileFormat.Json
            ? $"{Name}{Format.AsFileExtension()}"
            : $"{Name}{Format.AsFileExtension()}";
}
