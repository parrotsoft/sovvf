﻿using SO115App.API.Models.Classi.Condivise;
using System.Collections.Generic;

namespace SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Gac
{
    public interface IGetMezziById
    {
        List<Mezzo> Get(List<string> codiceMezzo);
    }
}
