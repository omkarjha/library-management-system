export interface Book {
  id: string;
  title: string;
  author: string;
  isbn: string;
  totalCopies: number;
  availableCopies: number;
}

export interface Member {
  id: string;
  name: string;
  email: string;
  joinedAt: string;
}

export interface BorrowRecord {
  id: string;
  bookId: string;
  memberId: string;
  borrowedAt: string;
  returnedAt: string | null;
}