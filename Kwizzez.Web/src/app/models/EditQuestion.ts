import EditAnswer from './EditAnswer';

export default interface EditQuestion {
  id: String | null;
  title: String;
  image: String | null;
  order: number;
  degree: number;
  answers: EditAnswer[];
}
