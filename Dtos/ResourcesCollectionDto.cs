namespace app_authors.Dtos
{
    public class ResourcesCollectionDto<T> : Resource where T : Resource
    {
        public List<T>? Values { get; set; }
    }
}