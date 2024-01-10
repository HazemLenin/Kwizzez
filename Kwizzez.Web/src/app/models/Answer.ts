export default interface Answer {
  id: string;
  questionId: string;
  title: string;
  order: number;
  isCorrect: boolean;
}
