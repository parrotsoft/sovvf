namespace SO115App.API.Models.AOP.Authorization
{
    using System.Security;
    using System.Security.Principal;
    using SO115App.API.Models.Classi.Autenticazione;
    using SO115App.API.Models.Servizi.CQRS.Queries;
    using SO115App.API.Models.Servizi.Infrastruttura;

    public class BoxMezziAuthorizationQueryHandlerDecorator<TQuery, TResult> : IBoxMezziQueryHandler<TQuery, TResult> 
        where TQuery : IBoxMezziQuery<TResult>
    {
        private readonly IBoxMezziQueryHandler<TQuery, TResult> decoratedHandler;
        private readonly IPrincipal currentUser;

        public BoxMezziAuthorizationQueryHandlerDecorator(IBoxMezziQueryHandler<TQuery, TResult> decoratedHandler,
            IPrincipal currentUser)
        { 
            this.decoratedHandler = decoratedHandler;
            this.currentUser = currentUser;
        }

        public TResult Handle(TQuery query)
        {
            this.Authorize();

            return this.decoratedHandler.Handle(query);
        }

        private void Authorize()
        {
            
            string username = this.currentUser.Identity.Name;
            
            if(this.currentUser.Identity.IsAuthenticated)
            {
                
                Utente user = Utente.FindUserByUsername(username);            
                if (user == null)
                    throw new SecurityException();

            }else
                throw new SecurityException();

        }
    }
}