using WebStorePrototype.Server.Models.CRM_API.Data;

namespace WebStorePrototype.Server.Models.Base
{
    public class GetListResponse<T>
    {
        private List<T> _items;
        private Pagination _pagination;
        public GetListResponse(List<T> items, Pagination pagination)
        {
            _items = items;
            _pagination = pagination;
        }

        public List<T> Items { get { return _items; } set { _items = value; } }
        public Pagination Pagination { get { return _pagination; } set { _pagination = value; } }

    }
}
