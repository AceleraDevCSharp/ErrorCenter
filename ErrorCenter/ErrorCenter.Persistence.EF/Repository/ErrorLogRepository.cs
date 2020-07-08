using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Repository.Model;
using ErrorCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorCenter.Persistence.EF.Repository
{
    public class ErrorLogRepository : IErrorLogRepository<ErrorLogModel>
    {
        protected DbContext _context;
        private List<ErrorLogModel> _errorsWithQuantityNotDuplicated = new List<ErrorLogModel>();
        private List<ErrorLogModel> _errorsWithQuantity = new List<ErrorLogModel>();

        public ErrorLogRepository(ErrorCenterDbContext context)
        {
            _context = context;
            QuantityErrorsByTitleNonDuplicated();
            QuantityErrorsByTitle();
        }

        public IEnumerable<ErrorLogModel> SelectAllWithoutFilter()
        {
            return  _errorsWithQuantity;
        }
        public List<ErrorLogModel> SelectAllWithoutDuplicated()
        {
            return _errorsWithQuantityNotDuplicated;
        }

        public List<ErrorLogModel> SelectByEnvironment(string whereEnvironment = null)
        {
            return _errorsWithQuantity.Where(x => x.Environment.Equals(whereEnvironment)).ToList();
        }
        public List<ErrorLogModel> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null)
        {
            switch (orderby)
            {
                case "Level":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment)).OrderBy(x => x.Level).ToList();
                case "Frequencia":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment)).OrderBy(x => x.Quantity).ToList();
                default:
                    return _errorsWithQuantity;
            }
        }
        public List<ErrorLogModel> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null)
        {
            switch (orderby)
            {
                case "Level":
                    if (whereSearch.Equals("Level"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else
                    {
                        return _errorsWithQuantityNotDuplicated;
                    }
                case "Frequencia":
                    if (whereSearch.Equals("Level"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else
                    {
                        return _errorsWithQuantityNotDuplicated;
                    }
                default:
                    return _errorsWithQuantityNotDuplicated;
            }
        }

        public List<ErrorLogModel> SelectOrderedBy(string orderby = null)
        {
            switch (orderby)
            {
                case "Level":
                    return _errorsWithQuantityNotDuplicated.OrderBy(x => x.Level).ToList();
                case "Frequencia":
                    return _errorsWithQuantityNotDuplicated.OrderBy(x => x.Quantity).ToList();
                default:
                    return _errorsWithQuantityNotDuplicated;
            }
        }
        public List<ErrorLogModel> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null)
        {
            switch (orderby)
            {
                case "Level":
                    if (whereSearch.Equals("Level"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Level.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Details.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Origin.Contains(searchText)).OrderBy(x => x.Level).ToList();
                    }
                    else
                    {
                        return _errorsWithQuantityNotDuplicated;
                    }
                case "Frequencia":
                    if (whereSearch.Equals("Level"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Level.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Details.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return _errorsWithQuantityNotDuplicated.Where(x => x.Origin.Contains(searchText)).OrderBy(x => x.Quantity).ToList();
                    }
                    else
                    {
                        return _errorsWithQuantityNotDuplicated;
                    }
                default:
                    return _errorsWithQuantityNotDuplicated;
            }
        }

        public List<ErrorLogModel> SelectSearchBy(string whereSearch = null, string searchText = null)
        {
            switch (whereSearch)
            {
                case "Level":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Level.Contains(searchText)).ToList();
                case "Descricao":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Details.Contains(searchText)).ToList();
                case "Origem":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Origin.Contains(searchText)).ToList();
                default:
                    return _errorsWithQuantityNotDuplicated;
            }
        }
        public List<ErrorLogModel> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null)
        {
            switch (whereSearch)
            {
                case "Level":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText)).ToList();
                case "Descricao":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText)).ToList();
                case "Origem":
                    return _errorsWithQuantityNotDuplicated.Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText)).ToList();
                default:
                    return _errorsWithQuantityNotDuplicated;
            }
        }


        private List<ErrorLogModel> QuantityErrorsByTitleNonDuplicated()
        {
            foreach (var error in _context.Set<ErrorLog>().ToList().OrderByDescending(x => x.CreatedAt))
            {
                foreach (var item in _errorsWithQuantity)
                {
                    if (error.Title == item.Title)
                    {
                        item.Quantity += 1;
                    }
                    else
                    {
                        _errorsWithQuantity.Add(new ErrorLogModel
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

            return  _errorsWithQuantityNotDuplicated;
        }
        private IEnumerable<ErrorLogModel> QuantityErrorsByTitle()
        {
            foreach (var error in _context.Set<ErrorLog>().ToList().OrderByDescending(x => x.CreatedAt))
            {
                foreach (var item in _errorsWithQuantity)
                {
                    ErrorLogModel aux = new ErrorLogModel
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
