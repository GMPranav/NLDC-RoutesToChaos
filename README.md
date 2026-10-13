# Routes to Chaos (NLDC-RoutesToChaos)

An interactive Windows Forms desktop application developed for the **Nonlinear Dynamics and Chaos (NLDC)** course to visualize, simulate, and explore canonical transitions to deterministic chaos in discrete dynamical systems (iterated maps).

- **Author:** Pranav G
- **Supervision:** Dr. Aravinda Raghavan

---

## Overview

In deterministic dynamical systems, the transition from regular (periodic or steady) behavior into deterministic chaos as a control parameter changes typically follows well-defined mathematical pathways known as **Routes to Chaos**. 

This application provides real-time time-series simulations and visual graphing for three classical routes to chaos:

1. **Period-Doubling Route** (Feigenbaum Cascade) - Illustrated via the **Logistic Map**
2. **Intermittency Route** (Pomeau–Manneville Type-I) - Illustrated via the **1D Quadratic Map**
3. **Crises Route** (Boundary Crisis) - Illustrated via the **2D Hénon Map**

---

## Mathematical Models & Routes to Chaos

### 1. Period-Doubling Route (Logistic Map)
The logistic map is a prototypical 1D model originally popularized in population biology (May, 1976):

$$x_{n+1} = r \cdot x_n (1 - x_n)$$

* **State Variable ($x$):** Normalized population ratio ($0 \le x \le 1$).
* **Control Parameter ($r$):** Growth rate ($0 \le r \le 4$).
* **Dynamics:**
  * $0 < r < 1$: Extinction ($x \to 0$).
  * $1 < r < 3$: Convergence to a stable fixed point $x^* = 1 - 1/r$.
  * $r = 3.0$: First period-doubling bifurcation to a 2-cycle.
  * $r \approx 3.449$: Bifurcation to a 4-cycle.
  * Successive bifurcations follow the universal **Feigenbaum constant** $\delta \approx 4.6692016...$
  * $r \approx 3.5699$: Onset of deterministic chaos.
  * $r \approx 3.8284$: Periodic window (period-3).

---

### 2. Intermittency Route (1D Quadratic Map)
Type-I Pomeau–Manneville intermittency occurs near a **tangent (saddle-node) bifurcation**:

$$x_{n+1} = 1 - r \cdot x_n^2$$

* **State Variable ($x$):** Dynamic state coordinate (bounded within $[-1, 1]$ for $r \le 2$).
* **Control Parameter ($r$):** Mapping parameter ($0 \le r \le 2$).
* **Dynamics:**
  * At $r = 1.75$, a stable period-3 cycle is born via a tangent bifurcation.
  * Just below this threshold (e.g., $r \approx 1.748$), a narrow "channel" or "ghost" of the fixed points remains between the function curve and the diagonal.
  * The trajectory spends long, laminar episodes slowly drifting through this channel, before escaping into turbulent, chaotic bursts across the state space, and eventually being reinjected back into the channel.

---

### 3. Crises Route (Hénon Map)
A **crisis** is a sudden change in or destruction of a chaotic attractor caused by its collision with an unstable periodic orbit or its manifold (Grebogi, Ott, and Yorke, 1982). This is modeled using the 2D Hénon mapping:

$$\begin{cases}
x_{n+1} = 1 - a \cdot x_n^2 + y_n \\
y_{n+1} = b \cdot x_n
\end{cases}$$

*(with classic dissipation parameter $b = 0.3$)*

* **State Variables ($x, y$):** Phase space coordinates.
* **Control Parameter ($a$):** Nonlinearity parameter ($0 \le a \le 1.42$).
* **Dynamics:**
  * For $a = 1.4$ and $b = 0.3$, the system forms the famous fractal **Hénon strange attractor**.
  * At $a \approx 1.42$, a **boundary crisis** occurs: the chaotic attractor collides with the basin boundary formed by the saddle fixed point and its stable manifold.
  * For $a > 1.42$, the attractor is destroyed. Trajectories experience transient chaos before escaping to infinity ($\infty$).

---

## Application Features

* **Interactive Parameter Tuning:** Adjust iteration count ($N$), initial states ($x_0, y_0$), and rates ($r$ / $a$) with real-time graph updates.
* **Smart Parameter Clamping:** Control ranges and step sizes automatically adjust to the selected model to avoid divergence and provide appropriate precision.
* **Adaptive Axis Scaling:** Centered coordinate axes with appropriate bounds:
  * Logistic: $[0, 1.0]$
  * Quadratic & Hénon: $[-1.5, 1.5]$
* **Responsive Layout:** The graphing viewport dynamically adjusts when resizing the application window.

---

## Quick Exploration Guide

Try these parameter sets in the application to observe key dynamical phenomena:

| Model | Parameter | Initial State $x(0), y(0)$ | Observed Phenomenon |
| :--- | :--- | :--- | :--- |
| **Period Doubling** | $r = 2.8$ | $x(0) = 0.5$ | Stable fixed point (period-1) |
| **Period Doubling** | $r = 3.2$ | $x(0) = 0.5$ | Stable 2-cycle oscillation |
| **Period Doubling** | $r = 3.5$ | $x(0) = 0.5$ | Stable 4-cycle oscillation |
| **Period Doubling** | $r = 3.8$ | $x(0) = 0.5$ | Fully chaotic trajectory |
| **Period Doubling** | $r = 3.83$ | $x(0) = 0.5$ | Stable period-3 window |
| **Intermittency** | $r = 1.748$ | $x(0) = 0.5$ | Type-I Intermittency (laminar phases + chaotic bursts) |
| **Intermittency** | $r = 1.750$ | $x(0) = 0.5$ | Tangent bifurcation into stable 3-cycle |
| **Crises** | $a = 1.40$ | $x(0) = 0.1, y(0) = 0.1$ | Classic Hénon chaotic attractor |
| **Crises** | $a = 1.425$ | $x(0) = 0.1, y(0) = 0.1$ | Boundary Crisis (transient chaos followed by escape) |

---

## Build and Run

### Prerequisites
* Windows OS
* .NET Framework 4.7.2
* Visual Studio (with .NET desktop development) or MSBuild

### Building via MSBuild
From the repository root:
```powershell
& "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" NLDC-RoutesToChaos.sln
```

### Running the Application
The compiled executable will be located at:
```powershell
.\NLDC-RoutesToChaos\bin\Debug\NLDC-RoutesToChaos.exe
```
