import QuestionForm from './AddQuestion';

export default interface AddQuiz {
  title: string;
  description: string | null;
  isPublic: boolean;
  questions: QuestionForm[];
}
