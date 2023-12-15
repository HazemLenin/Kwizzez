import Answer from './Answer';

export default interface Question {
  id: String;
  quizId: String;
  title: String;
  order: Number;
  degree: Number;
  answers: Answer[];
}
