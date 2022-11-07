using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicaparcial1.Data;
using practicaparcial1.Models;

namespace practicaparcial1.Controllers
{
    public class calculationsController : Controller
    {
        private readonly AppDbContext _context;

        public calculationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: calculations
        public async Task<IActionResult> Index()
        {
              return View(await _context.calc.ToListAsync());
        }

        // GET: calculations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.calc == null)
            {
                return NotFound();
            }

            var calculation = await _context.calc
                .FirstOrDefaultAsync(m => m.CalculationID == id);
            if (calculation == null)
            {
                return NotFound();
            }

            return View(calculation);
        }

        // GET: calculations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: calculations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalculationID,Operation,NumberA,NumberB,Result")] calculation calc)
        {
            int answer_int = 0;
            double answer_double = 0;
       
            calculation newcalc = new calculation();
            if (ModelState.IsValid)
            {
                if (calc.NumberB == null)
                {
                    calc.NumberB = 0;
                }

                switch (calc.Operation)  //
                {
                    case (Op)0:  // SUMAR

                        answer_int = calc.NumberA + calc.NumberB; //Respuesta de la suma A + B

                        //Aqui se crea un nuevo objeto para guardar los resultados
                        newcalc = new calculation()
                        {
                            CalculationID = calc.CalculationID,
                            Operation = calc.Operation,
                            NumberA = calc.NumberA,
                            NumberB = calc.NumberB,
                            Result = "La suma de " + calc.NumberA + " + " + calc.NumberB + " = " + answer_int, // La suma de 3 + 5 = 8
                        };
                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get
                        break;  


                    case (Op)1: // RESTAR
                        answer_int = calc.NumberA - calc.NumberB; //Respuesta de la resta A - B

                        //Aqui se crea un nuevo objeto para guardar los resultados
                        newcalc = new calculation()
                        {
                            CalculationID = calc.CalculationID,
                            Operation = calc.Operation,
                            NumberA = calc.NumberA,
                            NumberB = calc.NumberB,
                            Result = "La resta de " + calc.NumberA + " - " + calc.NumberB + " = " + answer_int, // La resta de 7 - 2 = 5 
                        };
                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get
                        break;  // sub

                    case (Op)2: //DIVISION
                        if(calc.NumberB != 0)
                        {
                            answer_double = Convert.ToDouble(calc.NumberA) / Convert.ToDouble(calc.NumberB);
                            //Aqui se crea un nuevo objeto para guardar los resultados
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "La division de " + calc.NumberA + " / " + calc.NumberB + " = " + answer_double, // La divion de 8 / 4 = 2
                            };
                            //Abajo se añade a la DB y se guarda los cambios                           
                        }
                        else
                        {
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "El numero B no puede ser 0" // La divion de 8 / 4 = 2
                            };
                        }
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get

                        break;  // div


                    case (Op)3: //MULTIPLICACION
                        answer_int = calc.NumberA * calc.NumberB; //Respuesta de la multiplicacion A * B

                        //Aqui se crea un nuevo objeto para guardar los resultados
                        newcalc = new calculation()
                        {
                            CalculationID = calc.CalculationID,
                            Operation = calc.Operation,
                            NumberA = calc.NumberA,
                            NumberB = calc.NumberB,
                            Result = "La multiplicacion de " + calc.NumberA + " * " + calc.NumberB + " = " + answer_int, // La multiplicacion 3 * 7 = 21
                        };
                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get
                        break;  // mul

                    case (Op)4: //RAIZ CUADRADA
                        if (calc.NumberA >= 0)
                        {
                            answer_double = Math.Sqrt(Convert.ToDouble(calc.NumberA));
                            //Aqui se crea un nuevo objeto para guardar los resultados
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "La raiz cuadrada de " + calc.NumberA + " = " + answer_double, // La raiz cuadrada de 121 es 11
                            };
                            //Abajo se añade a la DB y se guarda los cambios                           
                        }
                        else
                        {
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "El numero A debe ser positivo" 
                            };
                        }
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get
                        break;  // sqr

                    case (Op)5: // Logica NOT
                        if(calc.NumberA == 0 || calc.NumberA == 1)
                        {
                            if (calc.NumberA == 0)
                            { answer_int = 1; }
                            else
                            { answer_int = 0; }

                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "El negativo de " + calc.NumberA + " = " + answer_int, 
                            };                          
                        }
                        else
                        {
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "No se permiten numeros distintos 0 o 1",
                            };
                        }
                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); //Get
                        break;  // not

                    case (Op)6:

                        //0 0 = 0
                        //0 1 = 0
                        //1 0 = 0
                        //1 1 = 1

                        // si (num A == 0 || numA == 1) && ((num B == 0 || numB == 1)
                        // si (num A == 1 && num B == 1)
                        //     result = 1
                        // else 
                        //     result = 0
                        if ((calc.NumberA == 0 || calc.NumberA == 1) && (calc.NumberB == 0 || calc.NumberB == 1))
                        {
                            if (calc.NumberA == 1 && calc.NumberB == 1)
                            {
                                answer_int = 1; //Respuesta AND 1
                            }
                            else
                            {
                                answer_int = 0; //Respuesta AND 0
                            }

                            //Aqui se crea un nuevo objeto para guardar los resultados
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "AND: " + calc.NumberA + " y " + calc.NumberB + " = " + answer_int.ToString(), //AND: 1 y 1 = 1
                            };
                        }
                        else
                        {
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "AND:  No se permite numeros distintos a 0 o 1", //AND: No se permite numeros distintos a 0 o 1
                            };
                        }

                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                        break;  // and
                               

                    case (Op)7: // Logica OR
                        if ((calc.NumberA == 0 || calc.NumberA == 1) && (calc.NumberB == 0 || calc.NumberB == 1))
                        {
                            if (calc.NumberA == 0 && calc.NumberB == 0)
                            {
                                answer_int = 0; //Respuesta OR 0
                            }
                            else
                            {
                                answer_int = 1; //Respuesta OR 1
                            }
                            //Aqui se crea un nuevo objeto para guardar los resultados
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "OR: " + calc.NumberA + " o " + calc.NumberB + " = " + answer_int.ToString(), //OR: 1 o 0 = 1
                            };
                        }
                        else
                        {
                            newcalc = new calculation()
                            {
                                CalculationID = calc.CalculationID,
                                Operation = calc.Operation,
                                NumberA = calc.NumberA,
                                NumberB = calc.NumberB,
                                Result = "AND:  No se permite numeros distintos a 0 o 1", //AND: No se permite numeros distintos a 0 o 1
                            };                           
                        }
                        //Abajo se añade a la DB y se guarda los cambios
                        _context.Add(newcalc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                        break;  // or

                    default: break;
                }

            }
            return View(calc);
        }

        // GET: calculations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.calc == null)
            {
                return NotFound();
            }

            var calculation = await _context.calc.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }
            return View(calculation);
        }

        // POST: calculations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalculationID,Operation,NumberA,NumberB,Result")] calculation calculation)
        {
            if (id != calculation.CalculationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calculation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!calculationExists(calculation.CalculationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calculation);
        }

        // GET: calculations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.calc == null)
            {
                return NotFound();
            }

            var calculation = await _context.calc
                .FirstOrDefaultAsync(m => m.CalculationID == id);
            if (calculation == null)
            {
                return NotFound();
            }

            return View(calculation);
        }

        // POST: calculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.calc == null)
            {
                return Problem("Entity set 'AppDbContext.calc'  is null.");
            }
            var calculation = await _context.calc.FindAsync(id);
            if (calculation != null)
            {
                _context.calc.Remove(calculation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool calculationExists(int id)
        {
          return _context.calc.Any(e => e.CalculationID == id);
        }
    }
}
