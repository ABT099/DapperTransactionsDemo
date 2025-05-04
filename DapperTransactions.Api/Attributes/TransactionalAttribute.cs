using DapperTransactions.Data.DbTransactions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DapperTransactions.Api.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class TransactionalAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var unitOfWork = (ITransactionManager)context.HttpContext.RequestServices.GetService(typeof(ITransactionManager))!;

        try
        {
            unitOfWork.StartTransaction();
                
            var resultContext = await next();
                
            if (resultContext.Exception == null)
            {
                unitOfWork.CommitTransaction();
            }
            else
            {
                unitOfWork.RollbackTransaction();
            }
        }
        catch (Exception)
        {
            unitOfWork.RollbackTransaction();
            throw;
        }
    }
}