using DataAccess;
public readonly record struct BookDTO
(
    string Name,
    int Price,
    string FilePath,
    List<string> Authors,
    string Category
);