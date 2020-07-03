using ErrorCenter.Domain;
using ErrorCenter.IServices;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ErrorCenter.Services
{
    public class ErrorListingFilters : IErrorListingFilters<ErrorLogViewModel, string, string, string, string>
    {
        protected DbContext _context;
        private List<ErrorLogViewModel> _errorsWithQuantityNonDuplicated = new List<ErrorLogViewModel>();
        private List<ErrorLogViewModel> _errorsWithQuantity = new List<ErrorLogViewModel>();

        public ErrorListingFilters(ErrorCenterDbContext context)
        {
            _context = context;
            QuantityErrorsByTitleNonDuplicated();
            QuantityErrorsByTitle();
        }

        public List<ErrorLogViewModel> SelectAll()
        {
            return _errorsWithQuantity;
        }

        public List<ErrorLogViewModel> SelectAllOrderedBy(string order)
        {
            switch (order)
            {
                case "Level":
                    return _errorsWithQuantity
                        .OrderBy(x => x.Level)
                        .ToList();
                case "Frequencia":
                    return _errorsWithQuantityNonDuplicated
                        .OrderBy(x => x.Quantity)
                        .ToList();
                default:
                    return _errorsWithQuantity;
            }
        }

        public List<ErrorLogViewModel> SelectByEnvironment(string environment)
        {
            return _errorsWithQuantity
                .Select(x => x)
                .Where(x => x.Environment.Equals(environment))
                .ToList();
        }

        public List<ErrorLogViewModel> SelectByEnvironmentOrderedBy(string environment, string order)
        {
            switch (order)
            {
                case "Level":
                    return _errorsWithQuantity
                        .Select(x => x)
                        .Where(x => x.Environment.Equals(environment))
                        .OrderBy(x => x.Level)
                        .ToList();
                case "Frequencia":
                    return _errorsWithQuantityNonDuplicated
                        .Where(x => x.Environment.Equals(environment))
                        .OrderBy(x => x.Quantity)
                        .ToList();
                default:
                    return _errorsWithQuantity;
            }
        }

        public List<ErrorLogViewModel> SelectSearchBy(string typeSearch, string search)
        {
            throw new System.NotImplementedException();
        }

        public List<ErrorLogViewModel> SelectSearchByOrderBy(string typeSearch, string search, string order)
        {
            throw new System.NotImplementedException();
        }


        
        private List<ErrorLogViewModel> QuantityErrorsByTitleNonDuplicated()
        {
            foreach (var error in _context.Set<ErrorLog>().ToList().OrderBy(x => x.CreatedAt))
            {
                foreach (var item in _errorsWithQuantity)
                {
                    if (error.Title == item.Title)
                    {
                        item.Quantity += 1;
                    }
                    else
                    {
                        _errorsWithQuantity.Add(new ErrorLogViewModel
                        {
                            Id = error.Id,
                            Environment = error.Environment,
                            Level = error.Level,
                            Title = error.Title,
                            Details = error.Details,
                            CreatedAt = error.CreatedAt,
                            ArquivedAt = error.ArquivedAt,
                            DeletedAt = error.DeletedAt,
                            Origin = error.Origin,
                            IdUser = error.IdUser,
                            User = error.User,
                            Quantity = 1
                        });
                    }
                }
            }

            return _errorsWithQuantityNonDuplicated;
        }
        private List<ErrorLogViewModel> QuantityErrorsByTitle()
        {
            foreach (var error in _context.Set<ErrorLog>().ToList().OrderBy(x => x.CreatedAt))
            {
                foreach (var item in _errorsWithQuantity)
                {
                    ErrorLogViewModel aux = new ErrorLogViewModel
                    {
                        Id = error.Id,
                        Environment = error.Environment,
                        Level = error.Level,
                        Title = error.Title,
                        Details = error.Details,
                        CreatedAt = error.CreatedAt,
                        ArquivedAt = error.ArquivedAt,
                        DeletedAt = error.DeletedAt,
                        Origin = error.Origin,
                        IdUser = error.IdUser,
                        User = error.User,
                        Quantity = 1
                    };

                    foreach (var x in _errorsWithQuantity)
                    {
                        if (aux.Title.Equals(x.Title))
                        {
                            aux.Quantity += 1;
                        }
                    }

                    _errorsWithQuantity.Add(aux);
                }
            }

            return _errorsWithQuantity;
        }

    }
}
