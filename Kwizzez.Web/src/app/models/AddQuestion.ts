import AddAnswer from './AddAnswer';

export default interface AddQuestion {
  title: String;
  image: String | null;
  order: number;
  degree: number;
  answers: AddAnswer[];
}
