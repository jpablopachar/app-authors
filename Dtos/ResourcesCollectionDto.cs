namespace app_authors.Dtos
{
    public class ResourcesCollectionDto<T> : ResourceDto where T : ResourceDto
    {
        public List<T>? Values { get; set; }
    }
}