export class GetOccupationsResponse {

    constructor(
        public occupations?: string[],
        public errors?: string[]
    ) {}
}