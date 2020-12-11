import { IBook } from './book';
import { IAward } from './award';
import { IParticipation } from './participation';
import { IPublication } from './publication';

export interface IFilterResult {
    books?: IBook[];
    awards?: IAward[];
    participations?: IParticipation[];
    publications?: IPublication[];
};
