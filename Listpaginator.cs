// ====================================================================================
//    Author: Al-Khafaji, Ali Kifah
//    Date:   12.5.2023
//    Description: A generic class that allows paginating a list
// =====================================================================================

public class ListPaginator<T>
    {
        List<T> inputList;
        public int CurrentPageNum = 1;
        public int LastPageNum = 0;
        public int PageSize = 0;
        int start, count;
        public void SetPageSize(PagSize countPerPage)
        {
            this.PageSize = (int)countPerPage;
            this.LastPageNum = (int)Math.Ceiling((float)inputList.Count / (float)PageSize);
        }
        public ListPaginator(List<T> inputList, PagSize countPerPage = PagSize._10)
        {
            this.inputList = inputList;
            this.PageSize = (int)countPerPage;
            this.LastPageNum = (int)Math.Ceiling( (float)inputList.Count / (float)PageSize );
        }
        public ListPaginator(T[] inputArray, PagSize countPerPage = PagSize._10)
        {
            this.inputList = new List<T>(inputArray);
            this.PageSize = (int)countPerPage;
            this.LastPageNum = (int)Math.Ceiling((float)inputList.Count / (float)PageSize);
        }

        public List<T> GetPage(int pageNum)
        {
            if (inputList == null)
                return null;
            if (pageNum <= 0)
                pageNum = 1;
            if (pageNum > LastPageNum)
                pageNum = LastPageNum;
            if (pageNum == 1 && inputList.Count <= PageSize )
                return inputList;
            start = PageSize * (pageNum - 1);
            if (start > inputList.Count)
                return new List<T>();
            if (start + PageSize <= inputList.Count)
                count = PageSize;
            else
                count = inputList.Count - start ;
            return inputList.GetRange(start, count);
        }
        public List<T> FirstPage()
        {
            CurrentPageNum = 1;
            return GetPage(1);
        }
        public List<T> PreviousPage()
        {
            if (CurrentPageNum == 1)
                return FirstPage();
            CurrentPageNum--;
            return GetPage(CurrentPageNum);
        }
        public List<T> NextPage()
        {
            if (CurrentPageNum == LastPageNum)
                return LastPage();
            CurrentPageNum++;
            return GetPage(CurrentPageNum);
        }
        public List<T> LastPage()
        {
            CurrentPageNum = LastPageNum;
            return GetPage(LastPageNum);
        }
    }
