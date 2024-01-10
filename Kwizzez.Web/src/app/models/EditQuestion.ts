import EditAnswer from './EditAnswer';

export default interface EditQuestion {
  id: string | null;
  title: string;
  image: string | null;
  order: number;
  degree: number;
  answers: EditAnswer[];
}
