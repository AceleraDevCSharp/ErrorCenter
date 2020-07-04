using ErrorCenter.Domain;
using ErrorCenter.IServices;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace ErrorCenter.Services
{
    public class ErrorListingFilters : IErrorListingFilters<ErrorLogViewModel>
    {
        protected DbContext _context;
        private List<ErrorLogViewModel> _errorsWithQuantityNotDuplicated = new List<ErrorLogViewModel>();
        private List<ErrorLogViewModel> _errorsWithQuantity = new List<ErrorLogViewModel>();

        public ErrorListingFilters(ErrorCenterDbContext context)
        {
            _context = context;
            QuantityErrorsByTitleNonDuplicated();
            QuantityErrorsByTitle();
        }

        public List<ErrorLogViewModel> SelectAllWithoutFilter()
        {
            return _errorsWithQuantity;
        }
        public List<ErrorLogViewModel> SelectAllWithoutDuplicated()
        {
            return _errorsWithQuantityNotDuplicated;
        }
        public List<ErrorLogViewModel> SelectByEnvironmentOrderedBySearchby(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null)
        {
            if (whereEnvironment != null && orderby == null && whereSearch == null && searchText == null)
            {
                return _errorsWithQuantity.Where(x => x.Environment.Equals(whereEnvironment)).ToList();
            }
            else if (whereEnvironment != null && orderby != null && whereSearch == null && searchText == null)
            {
                switch (orderby)
                {
                    case "Level":
                        return _errorsWithQuantity.Where(x => x.Environment.Equals(orderby)).OrderBy(x => x.Level).ToList();
                    case "Frequência":
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(orderby)).OrderBy(x => x.Quantity).ToList();
                    default:
                        return _errorsWithQuantity;
                }
            }
            else if (whereEnvironment != null && orderby != null && whereSearch != null && searchText != null)
            {
                switch (orderby)
                {
                    case "Level":
                        if (whereSearch.Equals("Level"))
                        {
                            return _errorsWithQuantity.Where(x => x.Environment.Equals(orderby) && x.Level.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else if (whereSearch.Equals("Descrição"))
                        {
                            return _errorsWithQuantity.Where(x => x.Environment.Equals(orderby) && x.Details.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else if (whereSearch.Equals("Origem"))
                        {
                            return _errorsWithQuantity.Where(x => x.Environment.Equals(orderby) && x.Origin.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else
                        {
                            return _errorsWithQuantity;
                        }
                    case "Frequência":
                        if (whereSearch.Equals("Level"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(orderby) && x.Level.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else if (whereSearch.Equals("Descrição"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(orderby) && x.Details.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else if (whereSearch.Equals("Origem"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(orderby) && x.Origin.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else
                        {
                            return _errorsWithQuantity;
                        }
                    default:
                        return _errorsWithQuantity;
                }
            }
            else if (whereEnvironment == null && orderby != null && whereSearch == null && searchText == null)
            {
                switch (orderby)
                {
                    case "Level":
                        return _errorsWithQuantity.OrderBy(x => x.Level).ToList();
                    case "Frequência":
                        return _errorsWithQuantityNotDuplicated.OrderBy(x => x.Quantity).ToList();
                    default:
                        return _errorsWithQuantity;
                }
            }
            else if (whereEnvironment == null && orderby != null && whereSearch != null && searchText != null)
            {
                switch (orderby)
                {
                    case "Level":
                        if (whereSearch.Equals("Level"))
                        {
                            return _errorsWithQuantity.Where(x => x.Level.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else if (whereSearch.Equals("Descrição"))
                        {
                            return _errorsWithQuantity.Where(x => x.Details.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else if (whereSearch.Equals("Origem"))
                        {
                            return _errorsWithQuantity.Where(x => x.Origin.Contains(searchText)).OrderBy(x => x.Level).ToList();
                        }
                        else
                        {
                            return _errorsWithQuantity;
                        }
                    case "Frequência":
                        if (whereSearch.Equals("Level"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Level.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else if (whereSearch.Equals("Descrição"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Details.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else if (whereSearch.Equals("Origem"))
                        {
                            return _errorsWithQuantityNotDuplicated.Where(x => x.Origin.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                        }
                        else
                        {
                            return _errorsWithQuantity;
                        }
                    default:
                        return _errorsWithQuantity;
                }
            }
            else if (whereEnvironment == null && orderby == null && whereSearch != null && searchText != null)
            {
                switch (whereSearch)
                {
                    case "Level":
                        return _errorsWithQuantity.Where(x => x.Level.Contains(searchText)).ToList();
                    case "Descrição":
                        return _errorsWithQuantity.Where(x => x.Details.Contains(searchText)).ToList();
                    case "Origem":
                        return _errorsWithQuantity.Where(x => x.Origin.Contains(searchText)).ToList();
                    default:
                        return _errorsWithQuantity;
                }
                
            }
            else if (whereEnvironment != null && orderby == null && whereSearch != null && searchText != null)
            {
                switch (whereSearch)
                {
                    case "Level":
                        return _errorsWithQuantity.Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText)).ToList();
                    case "Descrição":
                        return _errorsWithQuantity.Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText)).ToList();
                    case "Origem":
                        return _errorsWithQuantity.Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText)).ToList();
                    default:
                        return _errorsWithQuantity;
                }

            }
            else
            {
                return _errorsWithQuantity;
            }
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

            return _errorsWithQuantityNotDuplicated;
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
