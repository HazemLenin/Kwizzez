import QuestionForm from './AddQuestion';

export default interface EditQuiz {
  id: String;
  title: String;
  description: String | null;
  questions: QuestionForm[];
}
