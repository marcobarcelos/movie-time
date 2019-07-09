using FreshMvvm;

namespace MovieTime.PageModels
{
    public class BasePageModel : FreshBasePageModel
    {
        public string Title { get; set; }

        public bool IsLoading { get; set; }
    }
}
