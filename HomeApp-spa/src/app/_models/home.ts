import { Photo } from './photo';

export interface Home {
    id: number;
    dateAdded: Date;
    street: string;
    schoolRating: string;
    city: string;
    state: string;
    price: number;
    bedrooms: number;
    bathrooms: number;
    daysOnMarket: number;
    squareFeet: number;
    coverUrl: number;
    numberOfPhotos: number;
    description?: string;
    renevated?: boolean;
    userId?: number;
    userEmail?: string;
    favUserIds?: number[];
    photos?: Photo[];
}

