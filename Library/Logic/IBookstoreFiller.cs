using LibraryLogic.Data;

namespace LibraryLogic.Logic
{
    public interface IBookstoreFiller
    {
        void Fill(Model.BookstoreDataContext state);
    }
}
