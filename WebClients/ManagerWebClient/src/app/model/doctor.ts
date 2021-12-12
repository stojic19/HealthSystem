import { Gender } from '../interfaces/patient';

export interface Doctor {
  id: number;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  gender: Gender;
  email: string;
  phoneNumber: string;
  street: string;
  streetNumber: string;
  cityId: number;
  city: any;
  specializationId: number;
  specialization: any;
}
