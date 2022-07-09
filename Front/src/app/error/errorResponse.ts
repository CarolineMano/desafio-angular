import { HttpHeaders } from "@angular/common/http";

export class ErrorResponse{
  error: any;
  headers!: HttpHeaders;
  message!: string;
  name!: string;
  ok!: boolean;
  status!: number;
  statusText!: string;
  url!: string;
}