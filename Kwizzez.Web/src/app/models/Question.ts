import Answer from './Answer';

export default interface Question {
  quizId: String;
  title: String;
  order: Number;
  degree: Number;
  answers: Answer[];
}
