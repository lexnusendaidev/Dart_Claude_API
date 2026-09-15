namespace DART_Claude.Models;

public interface IPathListItem
{
    int PathId { get; }
    string PathTypeName { get; }
    string PathLocation { get; }
}
