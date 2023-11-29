import QuestionForm from './AddQuestion';

export default interface AddQuiz {
  title: String;
  description: String | null;
  questions: QuestionForm[];
}
