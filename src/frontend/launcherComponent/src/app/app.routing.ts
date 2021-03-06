import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/auth/_guards';
import { RoutesPath } from './shared/enum/routes-path.enum';
import { Role } from './shared/model/utente.model';

const appRoutes: Routes = [
    { path: RoutesPath.Login, loadChildren: './features/login/login.module#LoginModule' },
    { path: RoutesPath.Home, loadChildren: './features/home/home.module#HomeModule', canActivate: [AuthGuard], data: { roles: [Role.Amministratore, Role.GestoreRichieste, Role.CallTaker] } },
    { path: RoutesPath.Autorimessa, loadChildren: './features/autorimessa/autorimessa.module#AutorimessaModule', canActivate: [AuthGuard], data: { roles: [Role.Amministratore, Role.GestoreRichieste, Role.CallTaker] } },
    { path: RoutesPath.Logged, loadChildren: './features/logged/logged.module#LoggedModule', canActivate: [AuthGuard] },
    { path: RoutesPath.Servizi, loadChildren: './features/servizi/servizi.module#ServiziModule', canActivate: [AuthGuard], data: { roles: [Role.Amministratore, Role.GestoreRichieste, Role.CallTaker] } },
    { path: RoutesPath.Statistiche, loadChildren: './features/statistiche/statistiche.module#StatisticheModule', canActivate: [AuthGuard] },
    { path: RoutesPath.GestioneUtenti, loadChildren: './features/gestione-utenti/gestione-utenti.module#GestioneUtentiModule', canActivate: [AuthGuard], data: { roles: [Role.Amministratore] } },
    { path: RoutesPath.NotFound, loadChildren: './features/not-found/not-found.module#NotFoundModule' },
    { path: '', pathMatch: 'full', redirectTo: RoutesPath.Home },
    { path: '**', redirectTo: RoutesPath.NotFound }
];

export const APP_ROUTING = RouterModule.forRoot(appRoutes, { enableTracing: false });
