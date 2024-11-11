using HumanResources.Core.Shared.Parameters;

namespace HumanResources.Core.Shared.Features;

public class PagedList<T> : List<T>
{
    public PagingData PagingData { get; set; }

    public PagedList(List<T> values, int count, int pageNumber, int pageSize)
    {
        PagingData = new PagingData()
        {
            Count = count,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            PageCount = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(values);
    }

    public PagedList(List<T> values, PagingData pagingData)
    {
        AddRange(values);
        PagingData = pagingData;
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, RequestParameters parameters)
    {
        if (parameters.PageNumber <= 0 || parameters.PageSize <= 0)
        {
            parameters.PageNumber = 1;
            parameters.PageSize = 10;
        }

        var count = source.Count();
        var items = source
        .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        .Take(parameters.PageSize)
        .ToList();

        var pagedList = new PagedList<T>(items, count, parameters.PageNumber, parameters.PageSize);

        return pagedList;
    }
}
