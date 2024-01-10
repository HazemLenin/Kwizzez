import AddAnswer from './AddAnswer';

export default interface AddQuestion {
  title: string;
  image: string | null;
  order: number;
  degree: number;
  answers: AddAnswer[];
}
