
export interface User {
    id: number;
    email: string;
    dateOfBirth: string;
    created: Date;
    roles?: string[];
}
