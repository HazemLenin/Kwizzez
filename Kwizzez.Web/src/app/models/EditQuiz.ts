import QuestionForm from './AddQuestion';

export default interface EditQuiz {
  id: string;
  title: string;
  description: string | null;
  questions: QuestionForm[];
}
