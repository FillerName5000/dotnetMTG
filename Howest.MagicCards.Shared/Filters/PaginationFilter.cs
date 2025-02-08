namespace Howest.MagicCards.Shared.Filters
{
    public class PaginationFilter
    {
        private int _pageSize = 150; /* actual max pageSize is set in the ApiBehaviourConf and is defined through the controller.
        However, a filter can't have parameters in its constructor, so dynamically setting the pagesize in here that way is not possible.
        MaxSize comparison will be done in every controller method, having the 150 as the default value*/
        // this is just a backup
        private int _pageNumber = 1;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = (value < 1) ? 1 : value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value >= 1) ? value : 1; }
        }
    }
}
