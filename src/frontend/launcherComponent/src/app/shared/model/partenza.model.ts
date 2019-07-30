import { Mezzo } from './mezzo.model';
import { Squadra } from './squadra.model';
import { TurnoPartenza } from './turno-partenza';

export class Partenza {
    constructor(
        public id: string,
        public squadre: Squadra[],
        public sganciata?: boolean,
        public mezzo?: Mezzo,
        public turno?: TurnoPartenza
    ) { }
}
