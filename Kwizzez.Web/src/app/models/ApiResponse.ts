interface ApiResponse {
  isSuccess: boolean;
  data: any;
  errors: Map<string, string[]>;
  pageIndex: number;
  totalPages: number;
  totalCount: number;
  hasPrevious: boolean;
  hasNext: boolean;
}
