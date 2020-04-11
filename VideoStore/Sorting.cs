using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public class Sorting
    {
        int _pages;
        int _count;
        int _pageSize;
        int _currentPage;
        bool _canNext;
        bool _canPrev;
        public int Pages 
        { 
            get 
            {
                return _pages;
            } 
        }

        public int PageSize 
        {
            get
            {
                return _pageSize;
            }
        }

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
        }

        public bool CanNext
        {
            get
            {
                return _canNext;
            }
        }

        public bool CanPrev
        {
            get
            {
                return _canPrev;
            }
        }

        string _genresSort;
        string _countrySort;

        public string GenresSort 
        {
            get
            {
                return _genresSort;
            }
        }

        public string CountrySort 
        {
            get
            {
                return _countrySort;
            }
        }

        public Sorting()
        {

        }
        public Sorting(int count, int pageSize, string genresSort = "none",string countrySort = "none")
        {
            _count = count;
            _pageSize = pageSize;

            _pages = (int)Math.Ceiling((decimal)((double)count / (double)pageSize));
            _currentPage = 1;

            _canNext = _pages > 1 ? true : false;
            _canPrev = false;

            _genresSort = genresSort;
            _countrySort = countrySort;
        }

        public void GoNext()
        {
            if(_currentPage < _pages)
            {
                _currentPage++;
                _canPrev = true;
            }
           

            if(_currentPage >= _pages)
            {
                _canNext = false;
            }
        }

        public void GoPrev()
        {
            if(_currentPage > 1)
            {
                _currentPage--;
                _canNext = true;
            }
          

            if (_currentPage <= 1)
            {
                _canPrev = false;
            }
        }

        public void Reset()
        {

        }
    }
}
