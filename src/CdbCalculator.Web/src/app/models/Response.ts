export interface Response<T>{
    dados: T;
    message: string;
    status: boolean
}