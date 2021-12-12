import { ISpecialization } from "./specialization";

export interface IDoctor {
  firstName: string;
  lastName: string;
  id: number;
  specialization : ISpecialization;
}
