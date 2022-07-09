import { Injectable } from '@angular/core';

@Injectable()
export class ErrorService {

  constructor() { }

  stringifyError(errorString: any) {
    let output = "";
    if(!errorString.includes("One or more validation errors occurred"))
      return errorString
    if(errorString.includes("The Cpf field is required"))
      output += "- O campo CPF é obrigatório\n"
    if(errorString.includes("O CPF deve conter 11 dígitos"))
      output += "- O CPF deve conter 11 dígitos\n"
    if(errorString.includes("The Nome field is required."))
      output += "- O campo nome é obrigatório\n"
    if(errorString.includes("O nome deve conter entre 2 e 100 caracteres"))
      output += "- O nome deve conter entre 2 e 100 caracteres\n"
    if(errorString.includes("The Email field is required"))
      output += "- O campo e-mail é obrigatório\n"
    if(errorString.includes("The Senha field is required"))
      output += "- O campo senha é obrigatório\n"
    if(errorString.includes("The ConfirmacaoSenha field is required"))
      output += "- O campo confirmação de senha é obrigatório\n"
    if(errorString.includes("ConfirmacaoSenha' and 'Senha' do not match."))
      output += "- Os campos senha e confirmação de senha não coincidem\n"
    if(errorString.includes("A unidade de medida deve conter entre 2 e 30 caracteres"))
      output += "- A unidade de medida deve conter entre 2 e 30 caracteres\n"
    if(errorString.includes("A descrição deve conter entre 2 e 100 caracteres"))
      output += "- A descrição deve conter entre 2 e 100 caracteres\n"
    if(errorString.includes("O nome da marca deve conter entre 2 e 50 caracteres"))
      output += "- O nome da marca deve conter entre 2 e 50 caracteres\n"
    if(errorString.includes("Deve ser um email no formato válido"))
      output += "- Deve ser um email no formato válido\n"
    return output
  }
}
