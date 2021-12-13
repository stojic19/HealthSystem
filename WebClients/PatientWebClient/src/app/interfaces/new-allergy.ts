import { IMedicationIngredient } from './medication-ingredient';

export interface INewAllergy {
  medicationIngredient: IMedicationIngredient;
  medicalIngredientId: number;
  medicalRecordId: number;
}
