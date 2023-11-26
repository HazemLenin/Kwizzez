import QuestionForm from './QuestionForm';

export default interface AddQuiz {
  title: String;
  description: String | null;
  questions: QuestionForm[];
}
