import QuestionForm from './QuestionForm';

export default interface QuizForm {
  title: String;
  hasLimitedTime: Boolean;
  publishDate: Date | null;
  expirationDate: Date | null;
  isPublic: Boolean;
  timeLimitTicks: Number;
  questions: QuestionForm[];
}
