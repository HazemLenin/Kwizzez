import Answer from './Answer';

export default interface Question {
  id: string;
  quizId: string;
  title: string;
  order: number;
  degree: number;
  answers: Answer[];
}
