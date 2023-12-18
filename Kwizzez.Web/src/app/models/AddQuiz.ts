import QuestionForm from './AddQuestion';

export default interface AddQuiz {
  title: string;
  description: string | null;
  questions: QuestionForm[];
}
